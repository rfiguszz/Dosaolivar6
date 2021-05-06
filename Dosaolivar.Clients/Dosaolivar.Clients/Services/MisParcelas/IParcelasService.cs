using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHotel.Clients.Core.Services.MisParcelas
{
    public interface IParcelasService
    {
        Task<IEnumerable<Models.ParcelasModel>> GetParcelasAsync();

        // Recoge todas las boquillas
        Task<IEnumerable<Models.BoquillasModel>> GetAllBoquillasAsync();
        Task<Models.BoquillasModel> GetOneBoquillaAsync(string _index);
        // Recoge todas los equipos
        Task<IEnumerable<Models.EquipoModel>> GetAllEquiposAsync();
        // Recoge todas los tratamientos
        Task<IEnumerable<Models.TratamientosModel>> GetAllTratamientosAsync();

        Task<Models.LocalidadModel> GetLocalidadByCPAsync(string cp);
        Task<Models.LocalidadModel> GetManualLocalidadByCPAsync(string cp);
       
        // INSERTA PARCELA
        Task<Models.GenericServiceAnswer> PostNewParcelaAsync(Models.ParcelasModel data, string apiUrl);
        // INSERTA EQUIPO
        Task<Models.GenericServiceAnswer> PostNewEquipoAsync(Models.EquipoModel data, string apiUrl);
        // INSERTA TRATAMIENTO
        Task<Models.GenericServiceAnswer> PostNewTratamientoAsync(Models.TratamientosModel data, string apiUrl);
        // BORRAR

        Task<Models.GenericServiceAnswer> DeleteParcelasAsync(string _id);

        Task<IEnumerable<Models.Producto>> GetAllProductosAsync();
    }
}