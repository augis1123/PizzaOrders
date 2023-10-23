using PizzaOrders.Data;
using PizzaOrders.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:3000") // add the allowed origins
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
// Add services to the container.
builder.Services.AddDbContext<PizzaOrdersDbContext>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IPizzaRepository, PizzaRepository>();
builder.Services.AddTransient<ISizeRepository, SizeRepository>();
builder.Services.AddTransient<IToppingRepository, ToppingRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<ISelectedToppingRepository, SelectedToppingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseRouting();
app.MapControllers();

app.UseCors(MyAllowSpecificOrigins);
app.UseCors("AllowAll");

app.Run();
