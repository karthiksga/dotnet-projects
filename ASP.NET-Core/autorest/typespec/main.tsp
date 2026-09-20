import "@typespec/http";
import "@typespec/openapi";
import "@typespec/openapi3";

using Http;
using OpenAPI;

@service(#{ title: "TestWebApi | v1" })
@info(#{ version: "1.0.0" })
@tagMetadata(#[#{ name: "TestWebApi" }])
namespace TestWebApiv1;

model WeatherForecast {
  date: plainDate;
  temperatureC: unknown;
  summary: null | string;
  temperatureF?: unknown;
}

@tag("TestWebApi")
@route("/weatherforecast")
@get
// FIXME: Open API operation 'GET /weatherforecast' is missing an operationId. Generated: 'get_weatherforecast'
op get_weatherforecast(): Body<WeatherForecast[]>;
