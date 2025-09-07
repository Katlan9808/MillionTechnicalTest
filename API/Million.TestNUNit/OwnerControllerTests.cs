using ApiMillion.Controllers;
using Microsoft.AspNetCore.Mvc;
using Million.Application.Interfaces;
using Million.Models.DTOs;
using Million.Models.Response;
using Moq;

[TestFixture]
public class OwnerControllerTests
{
    private Mock<IBaseService<OwnerDTO, string>> _mockService;
    private OwnerController _controller;

    [SetUp]
    public void Setup()
    {
        _mockService = new Mock<IBaseService<OwnerDTO, string>>();
        _controller = new OwnerController();
    }

    [Test]
    public async ValueTask GetAll_ReturnsOkWithOwners()
    {
        // Arrange
        var owners = new List<OwnerDTO>
        {
            new OwnerDTO { IdOwner = "OWN001", Name = "María López" },
            new OwnerDTO { IdOwner = "OWN002", Name = "Carlos Ruiz" }
        };

        _mockService.Setup(s => s.GetAsync()).ReturnsAsync(owners);

        // Act
        var result = await _controller.GetAll() as OkObjectResult;

        // Assert
        Assert.IsNotNull(result);
        var response = result.Value as RequestResultModel<IEnumerable<OwnerDTO>>;
        Assert.IsTrue(response.isSuccessful);
        Assert.AreEqual(2, ((List<OwnerDTO>)response.result).Count);
    }

    [Test]
    public async ValueTask GetById_ReturnsOk_WhenOwnerExists()
    {
        var owner = new OwnerDTO { IdOwner = "OWN001", Name = "María López" };
        _mockService.Setup(s => s.GetByIdAsync("OWN001")).ReturnsAsync(owner);

        var result = await _controller.GetById("OWN001") as OkObjectResult;

        Assert.IsNotNull(result);
        var response = result.Value as RequestResultModel<OwnerDTO>;
        Assert.IsTrue(response.isSuccessful);
        Assert.AreEqual("OWN001", response.result.IdOwner);
    }

    [Test]
    public async ValueTask GetById_ReturnsNotFound_WhenOwnerIsNull()
    {
        _mockService.Setup(s => s.GetByIdAsync("OWN999")).ReturnsAsync((OwnerDTO)null);

        var result = await _controller.GetById("OWN999");

        Assert.IsInstanceOf<NotFoundResult>(result);
    }

    [Test]
    public async ValueTask Create_ReturnsCreated_WhenOwnerIsValid()
    {
        var owner = new OwnerDTO { IdOwner = "OWN003", Name = "Laura Gómez" };
        _mockService.Setup(s => s.AddAsync(owner)).ReturnsAsync(owner);

        var result = await _controller.Create(owner) as CreatedAtActionResult;

        Assert.IsNotNull(result);
        var response = result.Value as RequestResultModel<OwnerDTO>;
        Assert.IsTrue(response.isSuccessful);
        Assert.AreEqual("OWN003", response.result.IdOwner);
    }

    [Test]
    public async ValueTask Update_ReturnsBadRequest_WhenIdMismatch()
    {
        var owner = new OwnerDTO { IdOwner = "OWN004", Name = "Mismatch" };

        var result = await _controller.Update("OWN999", owner);

        Assert.IsInstanceOf<BadRequestObjectResult>(result);
    }

    [Test]
    public async ValueTask Delete_ReturnsOk_WhenSuccessful()
    {
        _mockService.Setup(s => s.DeleteAsync("OWN001")).ReturnsAsync("OWN001");

        var result = await _controller.Delete("OWN001") as OkObjectResult;

        Assert.IsNotNull(result);
        Assert.AreEqual("OWN001", result.Value);
    }
}
