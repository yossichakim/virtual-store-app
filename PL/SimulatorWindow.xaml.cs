namespace PL;
using System.ComponentModel;
using System.Windows;
using BO;
using System.Diagnostics;



/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    public OrderDetails Order
    {
        get { return (OrderDetails)GetValue(OrderProperty); }
        set { SetValue(OrderProperty, value); }
    }

    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.Register("Order", typeof(OrderDetails), typeof(SimulatorWindow));

    public string Clock
    {
        get { return (string)GetValue(ClockProperty); }
        set { SetValue(ClockProperty, value); }
    }

    public static readonly DependencyProperty ClockProperty =
        DependencyProperty.Register("Clock", typeof(string), typeof(SimulatorWindow));

    public double ProgressBar
    {
        get { return (double)GetValue(ProgressBarProperty); }
        set { SetValue(ProgressBarProperty, value); }
    }

    public static readonly DependencyProperty ProgressBarProperty =
        DependencyProperty.Register("ProgressBar", typeof(double), typeof(SimulatorWindow));

    private int duration;

    private int precent;

    private Stopwatch stopwatch;

    private BackgroundWorker worker;

    private enum Progress { Clock, Update, Complete }

    public SimulatorWindow()
    {
        InitializeComponent();

        stopwatch = new Stopwatch();
        stopwatch.Start();
        Clock = stopwatch.Elapsed.ToString().Substring(0, 8);

        ProgressBar = 0;

        worker = new BackgroundWorker()
        {
            WorkerReportsProgress = true,
            WorkerSupportsCancellation = true
        };

        worker.DoWork += DoWork;
        worker.ProgressChanged += ProgressChanged;
        worker.RunWorkerCompleted += RunWorkerCompleted;
        worker.RunWorkerAsync();
    }
    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.RegisterToUpdtes(updateProgres);
        Simulator.Simulator.RegisterToStop(stopWorker);
        Simulator.Simulator.RegisterToComplete(UpdateComplete);

        Simulator.Simulator.StartSimulation();

        while (!worker.CancellationPending)
        {
            Thread.Sleep(1000);
            worker.ReportProgress((int)Progress.Clock);
        }

    }

    private void ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        switch ((Progress)e.ProgressPercentage)
        {
            case Progress.Clock:
                Clock = stopwatch.Elapsed.ToString().Substring(0, 8);
                ProgressBar = (precent++ * 100 / duration);
                break;
            case Progress.Update:
                Order = (e.UserState as OrderDetails)!;
                break;
            case Progress.Complete:
                break;
            default:
                break;
        }
    }

    private void RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.DeRegisterToUpdtes(updateProgres);
        Simulator.Simulator.DeRegisterToStop(stopWorker);
        Simulator.Simulator.DeRegisterToComplete(UpdateComplete);

        MessageBox.Show("Simulator stop");
        Close();
    }

    private void stopWorker()
    {
        worker.CancelAsync();
    }

    private void updateProgres(BO.Order order, OrderStatus? status, DateTime tretTime, int treatDuration)
    {
        precent = 0;
        duration = treatDuration;
        OrderDetails proc = new()
        {

            OrderID = order.OrderID,
            Status = order.Status,
            NextStatus = status,
            StatusDate = tretTime,
            ExecutionTime = tretTime + TimeSpan.FromSeconds(treatDuration)
        };

        worker.ReportProgress((int)Progress.Update, proc);
    }

    private void UpdateComplete(OrderStatus? status) 
    => worker.ReportProgress((int)Progress.Complete, status);

    private void Stop_Click(object sender, RoutedEventArgs e)
    => Simulator.Simulator.StopSimulation();
    
}

public class OrderDetails : DependencyObject
{
    public int OrderID { get; set; }
    public OrderStatus? Status { get; set; }
    public OrderStatus? NextStatus { get; set; }
    public DateTime StatusDate { get; set; }
    public DateTime ExecutionTime { get; set; }

}



