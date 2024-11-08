using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using tl2_tp5_2024_miguell29.Models;

namespace tl2_tp5_2024_miguell29.Repository
{
    public interface IProductoRepository
    {
        public List<Producto> GetProductos();
    }
}