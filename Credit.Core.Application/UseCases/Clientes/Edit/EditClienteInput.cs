namespace Credit.Core.Application.UseCases.Clientes.Edit
{
    public class EditClienteInput
    {
        public string Nome { get; set; } = string.Empty;

        public string Uf { get; set; } = string.Empty;

        public string Celular { get; set; } = string.Empty;
    }
}
