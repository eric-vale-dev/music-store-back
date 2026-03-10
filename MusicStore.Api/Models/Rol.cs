namespace MusicStore.Api.Models
{
    public class Rol
    {
        public int Id {get; set;}
        public string Nombre {get; set;} = string.Empty;

        //Navegación: un rol puede tener muchos usuarios
        public ICollection<Usuario> Usuarios {get; set;} = new List<Usuario>();
    }
}