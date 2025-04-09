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
            .GreaterThan(0)
            .WithMessage("ID записки должен быть больше 0.");
    }
} 