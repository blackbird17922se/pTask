namespace pTask.Model
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string PasswordHash { get; set; }  // Para almacenar la contraseña encriptada
    }
}
