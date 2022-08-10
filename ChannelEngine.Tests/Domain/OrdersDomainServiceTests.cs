using ChannelEngine.Domain.Entities;
using ChannelEngine.Domain.Services;
using FluentAssertions;

namespace ChannelEngine.Tests.Domain;

public class OrdersDomainServiceTests
{
    [Fact]
    public void GetTopSoldProduct_CorrectInputParams_ReturnsCorrectTopSoldProducts()
    {
        //Assess
        var sut = new OrdersDomainService();
        var orders = CreateOrders();
        const int topCount = 5;

        //Act
        var result = sut.GetTopSoldProducts(orders, topCount).ToList();


        //Assert
        var expected = CreateExpectedProductSales().ToList();

        result
            .Should()
            .Equal(expected, 
                (p1, p2) => p1.Product.MerchantProductNo == p2.Product.MerchantProductNo &&
                            p1.TotalSoldQuantity == p2.TotalSoldQuantity);
    }

    [Fact]
    public void GetTopSoldProduct_EmptyOrders_ReturnsEmptyTop()
    {
        //Assess
        var sut = new OrdersDomainService();
        var orders = new List<Order>();
        const int topCount = 5;

        //Act
        var result = sut.GetTopSoldProducts(orders, topCount).ToList();

        //Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void GetTopSoldProduct_NullOrders_ThrowsArgumentNullException()
    {
        //Assess
        var sut = new OrdersDomainService();
        List<Order> orders = null;
        const int topCount = 5;

        //Act
        Action act = () => sut.GetTopSoldProducts(orders, topCount).ToList();

        //Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void GetTopSoldProduct_NegativeTop_ThrowsArgumentOutOfRangeException()
    {
        //Assess
        var sut = new OrdersDomainService();
        List<Order> orders = CreateOrders();
        const int topCount = -5;

        //Act
        Action act = () => sut.GetTopSoldProducts(orders, topCount).ToList();

        //Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    private static List<Order> CreateOrders()
    {
        var productA = CreateProduct("A");
        var productB = CreateProduct("B");
        var productC = CreateProduct("C");
        var productD = CreateProduct("D");
        var productE = CreateProduct("E");
        var productF = CreateProduct("F");
        var productG = CreateProduct("G");

        var orderLineA = CreateOrderLine(productA, 10);
        var orderLineB = CreateOrderLine(productB, 10);
        var orderLineC = CreateOrderLine(productC, 10);
        var orderLineD = CreateOrderLine(productD, 10);
        var orderLineE = CreateOrderLine(productE, 10);
        var orderLineF = CreateOrderLine(productF, 10);
        var orderLineG = CreateOrderLine(productG, 10);

        var order1 = CreateOrder(orderLineA, orderLineA, orderLineB, orderLineB, orderLineB, orderLineC);
        var order2 = CreateOrder(orderLineD, orderLineD, orderLineE, orderLineE, orderLineF, orderLineF, orderLineG);
        var order3 = CreateOrder(orderLineA, orderLineA, orderLineB, orderLineG);
        var order4 = CreateOrder(orderLineA, orderLineA, orderLineB, orderLineE, orderLineF, orderLineF);

        return new List<Order> { order1, order2, order3, order4 };
    }

    private static IEnumerable<ProductSales> CreateExpectedProductSales()
    {
        return new List<ProductSales>
        {
            new()
            {
                Product = CreateProduct("A"),
                TotalSoldQuantity = 60
            },
            new()
            {
                Product = CreateProduct("B"),
                TotalSoldQuantity = 50
            },
            new()
            {
                Product = CreateProduct("F"),
                TotalSoldQuantity = 40
            },
            new()
            {
                Product = CreateProduct("E"),
                TotalSoldQuantity = 30
            },
            new()
            {
                Product = CreateProduct("D"),
                TotalSoldQuantity = 20
            }
        };
    }

    private static Order CreateOrder(params OrderLine[] orderLines)
    {
        return new Order { Lines = new List<OrderLine>(orderLines) };
    }

    private static Product CreateProduct(string name)
    {
        return new Product
        {
            MerchantProductNo = name,
            Description = $"{name} desc",
            Gtin = $"{name} gtin"
        };
    }

    private static OrderLine CreateOrderLine(Product product, int quantity)
    {
        return new OrderLine
        {
            Product = product,
            Quantity = quantity
        };
    }
}