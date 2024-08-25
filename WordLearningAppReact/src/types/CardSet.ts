import { Card } from "./Card";
import { v4 as uuidv4 } from "uuid";

export type CardSet = {
    id: string;
    name: string;
    cards: Card[];
};

const CreateSet = (name: string): CardSet => {
    return {
        id: uuidv4(),
        name: name,
        cards: []
    };
};

const DeleteSet = (cardSet: CardSet[], id: string): CardSet[] => {
    return cardSet.filter(cardSet => cardSet.id !== id);
}

export {
    CreateSet,
    DeleteSet
}