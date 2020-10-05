
using IdentityServer.Models.IdentityServer;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace IdentityServer.Models.ViewModels
{
    public class RoleViewModel : IValidatableObject
    {
        public RoleViewModel()
        {
        }
        public RoleViewModel(ApplicationRole role)
        {
            this.Id = role.Id;
            this.Name = role.Name;
            this.Description = role.Description;
        }


        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Remote("UniqueName", "Role", AdditionalFields = nameof(Id), ErrorMessage = "This name is already taken please choose another name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Description { get; set; }

        public ApplicationRole MapToModel()
        {
            var role = new ApplicationRole();

            if (string.IsNullOrEmpty(this.Id))
                role.Id = Guid.NewGuid().ToString();
            else
                role.Id = this.Id;
                
            role.Name = this.Name;
            role.Description = this.Description;
            return role;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            return results;
        }
      }
}

