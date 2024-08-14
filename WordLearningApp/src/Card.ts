import { v4 as uuidv4 } from "uuid";

export type Card = {
    id: string;
    word: string;
    translation: string;
};

const СreateCard = (word: string, translation: string): Card => {
    return {
        id: uuidv4(),
        word: word,
        translation: translation
    };
};

const UpdateCard = (cards: Card[], id: string, updatedWord?: string, updatedTranslation?: string): Card[] => {
    return cards.map(card => card.id === id? {
        ...card,
        word: updatedWord !== undefined ? updatedWord : card.word,
        translation: updatedTranslation !== undefined ? updatedTranslation : card.translation
        }
        : card
    );
};

const DeleteCard = (cards: Card[], id: string): Card[] => {
    return cards.filter(card => card.id !== id);
};

export {
    СreateCard,
    UpdateCard,
    DeleteCard
}