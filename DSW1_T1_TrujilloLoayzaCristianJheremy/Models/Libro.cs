using System.ComponentModel.DataAnnotations;

namespace DSW1_T1_TrujilloLoayzaCristianJheremy.Models
{
    public class Libro
    {
        [Display(Name = "ID")]
        public int Id { get; set; }

        [Display(Name = "Nombre del Libro")]
        [Required(ErrorMessage = "El nombre del libro es obligatorio.")]
        [StringLength(120, ErrorMessage = "El nombre del libro no puede superar los 120 caracteres.")]
        public string NombreLibro { get; set; } = string.Empty;

        [Display(Name = "Autor")]
        [Required(ErrorMessage = "El nombre del autor es obligatorio.")]
        [StringLength(100, ErrorMessage = "El autor no puede superar los 100 caracteres.")]
        public string Autor { get; set; } = string.Empty;

        [Display(Name = "Editorial")]
        [Required(ErrorMessage = "El nombre de la editorial es obligatorio.")]
        [StringLength(100, ErrorMessage = "La editorial no puede superar los 100 caracteres.")]
        public string NombreEditorial { get; set; } = string.Empty;

        [Display(Name = "Código ISDN")]
        [Required(ErrorMessage = "El código ISDN es obligatorio.")]
        [StringLength(20, ErrorMessage = "El código ISDN no puede superar los 20 caracteres.")]
        public string ISDN { get; set; } = string.Empty;
    }
}
