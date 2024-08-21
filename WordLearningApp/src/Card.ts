import { v4 as uuidv4 } from "uuid";
import { CardSet } from "./CardSet";

export type Card = {
    id: string;
    word: string;
    translation: string;
};

const CreateCard = (word: string, translation: string, cardSet: CardSet): CardSet => {
    const newCard: Card = {
        id: uuidv4(),
        word: word,
        translation: translation
    };

    return {
        ...cardSet,
        cards: [...cardSet.cards, newCard]
    };
};

const UpdateCard = (cardSet: CardSet, id: string, updatedWord?: string, updatedTranslation?: string): CardSet => {
    const updatedCards = cardSet.cards.map(card => card.id === id? {
        ...card,
        word: updatedWord !== undefined ? updatedWord : card.word,
        translation: updatedTranslation !== undefined ? updatedTranslation : card.translation
        }
        : card
    );

    return {
        ...cardSet,
        cards: updatedCards
    };
};

const DeleteCard = (cardSet: CardSet, id: string): CardSet => {
    const updatedCards = cardSet.cards.filter(card => card.id !== id);

    return {
        ...cardSet,
        cards: updatedCards
    };
};

export {
    CreateCard,
    UpdateCard,
    DeleteCard
}