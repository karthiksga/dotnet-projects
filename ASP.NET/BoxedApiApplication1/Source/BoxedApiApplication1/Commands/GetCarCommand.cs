namespace BoxedApiApplication1.Commands;

using System.Globalization;
using BoxedApiApplication1.Repositories;
using BoxedApiApplication1.ViewModels;
using Boxed.Mapping;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

public class GetCarCommand(
    IActionContextAccessor actionContextAccessor,
    ICarRepository carRepository,
    IMapper<Models.Car, Car> carMapper)
{
    private readonly IActionContextAccessor actionContextAccessor = actionContextAccessor;
    private readonly ICarRepository carRepository = carRepository;
    private readonly IMapper<Models.Car, Car> carMapper = carMapper;

    public async Task<IActionResult> ExecuteAsync(int carId, CancellationToken cancellationToken)
    {
        var car = await this.carRepository.GetAsync(carId, cancellationToken).ConfigureAwait(false);
        if (car is null)
        {
            return new NotFoundResult();
        }

        var httpContext = this.actionContextAccessor.ActionContext!.HttpContext;
        var ifModifiedSince = httpContext.Request.Headers.IfModifiedSince;
        if (ifModifiedSince.Count != 0 &&
            DateTimeOffset.TryParse(ifModifiedSince, out var ifModifiedSinceDateTime) &&
            (ifModifiedSinceDateTime >= car.Modified))
        {
            return new StatusCodeResult(StatusCodes.Status304NotModified);
        }

        var carViewModel = this.carMapper.Map(car);
        httpContext.Response.Headers.LastModified = car.Modified.ToString("R", CultureInfo.InvariantCulture);
        return new OkObjectResult(carViewModel);
    }
}
