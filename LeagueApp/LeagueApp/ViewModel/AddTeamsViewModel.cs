﻿using LeagueApp.Attributes;
using LeagueApp.Settings;
using System.ComponentModel.DataAnnotations;

namespace LeagueApp.ViewModel
{
    public class AddTeamsViewModel
    {
        public int Id { get; set; }
        [StringLength(50, MinimumLength =3)]
        public string Name { get; set; } = string.Empty;

        [AllowedExtensions(FileSettings.AllowedExtensions),
             MaxFileSize(FileSettings.MaxFileSiezInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}
