﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FurnitureDBLibrary.Models.Manufacturers
{
    public interface IRechicadrev
    {

        string ManufacturerName { get; }

        decimal ManufacturerMarkup { get; }

    }
}
