namespace GO.Domain
{
    public class Customer
    {
        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string CNPJ { get; set; }
        
        public string IE { get; set; }

        public string RazaoSocial { get; set; }

        public string Observacoes { get; set; }

        public string DDDTelefone { get; set; }
        
        public string Telefone { get; set; }

        public string DDDCelular { get; set; }

        public string Celular { get; set; }

        public byte TipoPessoa { get; set; }

        public Response Response { get; set; }
    }
}