import { Card } from "./Card";
import { v4 as uuidv4 } from "uuid";

export type CardSet = {
    id: string;
    name: string;
    cards: Card[];
};

const createSet = (name: string): CardSet => {
    return {
        id: uuidv4(),
        name: name,
        cards: []
    };
};

const deleteSet = (cardSets: CardSet[], id: string): CardSet[] => {
    return cardSets.filter(cardSets => cardSets.id !== id);
}

const markCardAsLearned = (cardSet: CardSet): CardSet => {
    const updatedCards = cardSet.cards.slice(1); 

    return {
        ...cardSet,
        cards: updatedCards
    };
}

const moveCardToBottom = (cardSet: CardSet): CardSet => {
    const [topCard, ...restCards] = cardSet.cards;

    return {
        ...cardSet,
        cards: [...restCards, topCard] 
    };
};

export {
    createSet,
    deleteSet, 
    markCardAsLearned, 
    moveCardToBottom
}