namespace GameStore.Api.Dtos;

//A DTO (Data Transfer Object) is a simple object that is used to transfer data between different layers of an application.
// It is often used to encapsulate data that is sent from the client to the server, or from the server to the client. In this case,
// we are defining a GameSummaryDto class that will be used to transfer data about games in our API.
public record  GameSummaryDto(
    int Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly  ReleaseDate
);
