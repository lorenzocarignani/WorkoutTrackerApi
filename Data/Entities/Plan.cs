﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WorkoutTrackerApi.Data.Enums;

namespace WorkoutTrackerApi.Data.Entities
{
    public class Plan
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int PlanId { get; set; }

        [Required]
        [StringLength(100)] // Limitar longitud del nombre del plan
        public string PlanName { get; set; }

        public string? PlanDescription { get; set; }

        public DateTime PlanDate { get; set; } // Cambio de PlansDate a PlanDate para consistencia

        public PlanState PlanState { get; set; } = PlanState.Pending;

        // Relación uno a muchos: Un plan pertenece a un usuario
        public int UserId { get; set; }
        public User User { get; set; }

        // Relación muchos a muchos: Un plan puede tener muchos ejercicios
        public ICollection<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}
