//-----------------------------------------------------------------------
// <copyright file="HostaliandoExceptionCode.cs" company="Gabriel Castillo">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Hostaliando.Business.Exceptions
{
    /// <summary>
    /// Business exception codes
    /// </summary>
    public enum HostaliandoExceptionCode
    {
        /// <summary>
        /// A bad argument exception
        /// </summary>
        BadArgument = 1,

        /// <summary>
        /// The invalid foreign key
        /// </summary>
        InvalidForeignKey = 50,

        /// <summary>
        /// The user email already exists
        /// </summary>
        UserEmailAlreadyExists = 100,

        /// <summary>
        /// Exceeded the room capacity
        /// </summary>
        ExceededRoomCapacity = 200,

        /// <summary>
        /// The room is of another hostel
        /// </summary>
        RoomIsOfAnotherHostel = 201,

        /// <summary>
        /// Cant update email notification
        /// </summary>
        CantUpdateEmailNotification = 300
    }
}