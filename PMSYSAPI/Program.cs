using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PMSYSAPI.Data;
using PMSYSAPI.Mapping;
using PMSYSAPI.Repository.Implementations;
using PMSYSAPI.Repository.Interfaces;
using PMSYSAPI.Services.Implementations;
using PMSYSAPI.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Register Services
builder.Services.AddScoped<ICompanyService, CompanyService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectPhaseService, ProjectPhaseService>();
builder.Services.AddScoped<ICompanyGroupService, CompanyGroupService>();
builder.Services.AddScoped<IProjectStatusService, ProjectStatusService>();
builder.Services.AddScoped<IStoredProcedureService, StoredProcedureService>();

// Register Repositories
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<ICompanyGroupRepository, CompanyGroupRepository>();
builder.Services.AddScoped<IProjectPhaseRepository, ProjectPhaseRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();