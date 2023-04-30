namespace Credit.Core.Application.UseCases.Clientes.Create
{
    public class CreateClienteOutput
    {
        public Guid Uid { get; set; }

        public string Cpf { get; set; } = string.Empty;

        public string Nome { get; set; } = string.Empty;

        public string Uf { get; set; } = string.Empty;

        public string Celular { get; set; } = string.Empty;
    }
}
