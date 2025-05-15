using FluentValidation;

namespace Notes.Application.Features.Notes.Commands.DeleteNote;

/// <summary>
/// Валидатор команды удаления записки.
/// </summary>
public class DeleteNoteCommandValidator : AbstractValidator<DeleteNoteCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="DeleteNoteCommandValidator"/>.
    /// </summary>
    public DeleteNoteCommandValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("ID записки должен быть пустым.");
    }
} 