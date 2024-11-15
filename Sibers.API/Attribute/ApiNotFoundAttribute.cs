using Sibers.Api.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Sibers.Api.Attribute
{

    /// <summary>
    /// Фильтр, который определяет тип значения и код состояния 404, возвращаемый действием <see cref="ApiExceptionDetail"/>
    /// </summary>
    public class ApiNotFoundAttribute : ProducesResponseTypeAttribute
    {
        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiNotFoundAttribute"/>
        /// </summary>
        public ApiNotFoundAttribute() : this(typeof(ApiExceptionDetail))
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="ApiNotFoundAttribute"/>
        /// </summary>
        public ApiNotFoundAttribute(Type type)
            : base(type, StatusCodes.Status404NotFound)
        {
        }
    }
}
