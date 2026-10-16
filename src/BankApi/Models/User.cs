using System.ComponentModel.DataAnnotations;

namespace BankApi.Models
{
    public enum UserStatus
    {
        PhoneVerified = 1,   // Телефон введен и подтвержден СМС, но пароля и профиля еще нет
        PendingDetails = 2,  // Ввел пароль, но еще не заполнил профиль (ФИО, паспорт)
        Active = 3,          // Регистрация полностью завершена, доступ в банк открыт
        Blocked = 4          // Аккаунт временно или навсегда заблокирован службой безопасности
    }
    public class BankUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Phone { get; set; } = string.Empty; // Храним чистые цифры: 79990000000  

        [Required]
        public UserStatus Status { get; set; } = UserStatus.PhoneVerified; 

        // Поля ниже могут быть null на начальных этапах воронки регистрации
        public string? PasswordHash { get; set; } 

        public string? LastName { get; set; }      // Фамилия
        public string? FirstName { get; set; }     // Имя
        public string? MiddleName { get; set; }    // Отчество (может быть null)
        public bool HasNoMiddleName { get; set; }  // Галочка "Нет отчества"
        public string? Email { get; set; }         // Почта
        public DateTime? BirthDate { get; set; }   // Дата рождения

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string? SessionToken { get; set; } // Хранилище уникального токена сессии

    }
}