using FluentValidation;

namespace Notes.Application.Features.Notes.Commands.UpdateNote;

/// <summary>
/// Валидатор команды обновления записки.
/// </summary>
public class UpdateNoteCommandValidator : AbstractValidator<UpdateNoteCommand>
{
    /// <summary>
    /// Инициализирует новый экземпляр класса <see cref="UpdateNoteCommandValidator"/>.
    /// </summary>
    public UpdateNoteCommandValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0)
            .WithMessage("ID записки должен быть больше 0.");

        RuleFor(x => x.Data.Content)
            .NotEmpty()
            .WithMessage("Содержание записки не может быть пустым.");

        RuleFor(x => x.Data.Tags)
            .NotNull()
            .WithMessage("Коллекция тэгов не может быть null.");
    }
} 