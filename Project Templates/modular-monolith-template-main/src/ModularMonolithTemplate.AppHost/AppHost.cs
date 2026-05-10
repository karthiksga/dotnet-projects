var builder = DistributedApplication.CreateBuilder(args);

var postgres = builder.AddPostgres("Postgres")
	.WithDataVolume("modular-monolith-postgres-data");

builder.AddProject<Projects.ModularMonolith_Host>("modular-monolith-host")
	.WithReference(postgres);

await builder.Build().RunAsync();
