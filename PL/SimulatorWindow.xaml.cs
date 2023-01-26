namespace PL;

using BO;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;

/// <summary>
/// Interaction logic for SimulatorWindow.xaml
/// </summary>
public partial class SimulatorWindow : Window
{
    /// <summary>
    /// Represents the order details, including the order ID, status, next status, status date and execution time.
    /// </summary>
    public OrderDetails Order
    {
        get { return (OrderDetails)GetValue(OrderProperty); }
        set { SetValue(OrderProperty, value); }
    }

    public static readonly DependencyProperty OrderProperty =
        DependencyProperty.Register("Order", typeof(OrderDetails), typeof(SimulatorWindow));

    /// <summary>
    /// Represents the clock.
    /// </summary>
    public string Clock
    {
        get { return (string)GetValue(ClockProperty); }
        set { SetValue(ClockProperty, value); }
    }

    public static readonly DependencyProperty ClockProperty =
        DependencyProperty.Register("Clock", typeof(string), typeof(SimulatorWindow));

    /// <summary>
    /// Represents the progress bar.
    /// </summary>
    public double ProgressBar
    {
        get { return (double)GetValue(ProgressBarProperty); }
        set { SetValue(ProgressBarProperty, value); }
    }

    public static readonly DependencyProperty ProgressBarProperty =
        DependencyProperty.Register("ProgressBar", typeof(double), typeof(SimulatorWindow));

    private int length = 1;

    private int proportion;

    private Stopwatch stopwatch;

    private BackgroundWorker worker;

    private enum Progress
    { Clock, Update, Complete }

    /// <summary>
    /// Initializes a new instance of the SimulatorWindow class.
    /// </summary>
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

    /// <summary>
    /// Perform the simulation work.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.Simulator.UpdateOrder += updateProgres;
        Simulator.Simulator.StopSim += stopWorker;
        Simulator.Simulator.UpdateComplete += UpdateComplete;

        Simulator.Simulator.StartSimulation();

        while (!worker.CancellationPending)
        {
            worker.ReportProgress((int)Progress.Clock);
            Thread.Sleep(1000);
        }
    }

    /// <summary>
    /// Reports the progress of the simulation.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {
        Progress progress = (Progress)e.ProgressPercentage;

        if (progress == Progress.Clock)
        {
            Clock = stopwatch.Elapsed.ToString().Substring(0, 8);
            ProgressBar = (proportion++ * 100 / length);
        }
        else
        {
            Order = (e.UserState as OrderDetails)!;
        }
    }

    /// <summary>
    /// Perform cleanup work when the simulation is completed.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.Simulator.UpdateOrder -= updateProgres;
        Simulator.Simulator.StopSim -= stopWorker;
        Simulator.Simulator.UpdateComplete -= UpdateComplete;

        MessageBox.Show("Simulator as been stop", "Simulator Stop", MessageBoxButton.OK, MessageBoxImage.Information);

        Close();
    }

    /// <summary>
    /// Stops the worker that is running the simulation.
    /// </summary>
    private void stopWorker()
    {
        worker.CancelAsync();
    }

    /// <summary>
    /// Updates the progress of the simulation.
    /// </summary>
    /// <param name="order">The order that is being processed.</param>
    /// <param name="status">The status of the order after being processed.</param>
    /// <param name="tretTime">The time the order was processed.</param>
    /// <param name="treatDuration">The length of processing the order.</param>
    private void updateProgres(BO.Order order, OrderStatus? status, DateTime tretTime, int treatDuration)
    {
        proportion = 0;
        length = treatDuration;

        OrderDetails proc = new()
        {
            OrderID = order.OrderID,
            Status = order.Status,
            NextStatus = status,
            StatusDate = tretTime,
            ExecutionTime = tretTime + TimeSpan.FromSeconds(treatDuration)
        };

        if (worker.IsBusy)
        {
            worker.ReportProgress((int)Progress.Update, proc);
        }
    }

    /// <summary>
    /// Reports the completion of the simulation.
    /// </summary>
    /// <param name="status">The final status of the simulation.</param>
    private void UpdateComplete(OrderStatus? status)
    {
        if (worker.IsBusy)
        {
            worker.ReportProgress((int)Progress.Complete, status);
        }
    }

    /// <summary>
    /// Stops the simulation.
    /// </summary>
    /// <param name="sender">The source of the event.</param>
    /// <param name="e">The event data.</param>
    private void Stop_Click(object sender, RoutedEventArgs e)
    => Simulator.Simulator.StopSimulation();
}

/// <summary>
/// Represents the details of an order including the order ID, status, next status, status date and execution time.
/// </summary>
public class OrderDetails : DependencyObject
{
    /// <summary>
    /// Represents the Order ID.
    /// </summary>
    public int OrderID { get; set; }

    /// <summary>
    /// Represents the status of the order.
    /// </summary>
    public OrderStatus? Status { get; set; }

    /// <summary>
    /// Represents the next status of the order.
    /// </summary>
    public OrderStatus? NextStatus { get; set; }

    /// <summary>
    /// Represents the status date of the order.
    /// </summary>
    public DateTime StatusDate { get; set; }

    /// <summary>
    /// Represents the execution time of the order.
    /// </summary>
    public DateTime ExecutionTime { get; set; }
}