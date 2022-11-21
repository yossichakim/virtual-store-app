using BLApi;
using Dal;
using DalApi;

namespace BlImplementation;

internal class Order : IOrder
{
    private IDal Dal = new DalList();

}
