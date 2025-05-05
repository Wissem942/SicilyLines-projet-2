using ApiClient;


var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/clientitems", () => ClientDAO.getClient());
app.MapGet("/resaitems", () => ReservationDAO.getReservation());
//app.MapGet("/traverseitems", () => TraverseDAO.getTraverse());


app.MapGet("/clientitems/{pseudo}", (string pseudo) =>
    ClientDAO.trouvePseudo(pseudo)
        is Client u
            ? Results.Ok(u)
            : Results.NotFound());

app.MapGet("/resaitems/{idClient}", (int idClient) =>
    ReservationDAO.trouveReservation(idClient)
        is List<Reservation> r
            ? Results.Ok(r)
            : Results.NotFound());

app.MapGet("/traverseitems/", () =>
    TraverseDAO.trouveTraverse()
        is List<Traverse> r
            ? Results.Ok(r)
            : Results.NotFound());

app.MapPut("/clientitems", (Client u, string num, string adr) =>
{
    ClientDAO.updateClient(u, num, adr);
});


app.Run();
