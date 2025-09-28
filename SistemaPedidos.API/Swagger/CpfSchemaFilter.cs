using System.Linq;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using SistemaPedidos.Application.DTOs.Cliente;

namespace SistemaPedidos.API.Swagger;

public class CpfSchemaFilter : ISchemaFilter
{
    public void Apply(OpenApiSchema schema, SchemaFilterContext context)
    {
        var targetTypes = new[]
        {
            typeof(ClienteCreateDTO),
            typeof(ClienteUpdateDTO),
            typeof(ClienteReadDTO),
            typeof(SistemaPedidos.Domain.Entities.Cliente)
        };

        if (!targetTypes.Contains(context.Type))
            return;

        foreach (var key in new[] { "CPF", "cpf" })
        {
            if (schema.Properties.TryGetValue(key, out var cpfProp))
            {
                cpfProp.Description = "CPF do cliente (apenas números). Exemplo: 01234567890";
                cpfProp.Example = new OpenApiString("01234567890");
                cpfProp.Type = "string";
            }
        }
    }
}