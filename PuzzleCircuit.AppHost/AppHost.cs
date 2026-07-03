IDistributedApplicationBuilder builder = DistributedApplication.CreateBuilder(args);

// Resource name "puzzlecircuit-server" must match the service discovery env var
// read in vite.config.ts: services__puzzlecircuit-server__https__0
IResourceBuilder<ProjectResource> api = builder
    .AddProject<Projects.PuzzleCircuit_API>("puzzlecircuit-server");

builder.AddNpmApp("puzzlecircuit-client", "../puzzlecircuit.client", "dev")
    .WithReference(api)
    .WaitFor(api)
    .WithHttpEndpoint(port: 64182, env: "DEV_SERVER_PORT")
    .PublishAsDockerFile();

builder.Build().Run();
