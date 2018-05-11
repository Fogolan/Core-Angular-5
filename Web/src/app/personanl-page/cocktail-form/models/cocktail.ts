import { IngredientDto } from '@app/shared/ingredients-list/models/ingredientDto';

export class Cocktail {
    public id: number;
    public name: string;
    public degrees: number;
    public amount: number;
    public imageSrc = 'http://res.cloudinary.com/fogolan/image/upload/v1525975481/nophoto_c3wkvb.jpg';
    public ingredients: IngredientDto[];
}
