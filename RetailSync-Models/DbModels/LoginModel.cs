namespace RetailSync_Models.DbModels
{
    public record LoginModel() 
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

}
