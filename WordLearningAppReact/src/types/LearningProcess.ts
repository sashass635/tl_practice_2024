import { Card } from "./Card";
import { CardSet} from "./CardSet"

export type LearningProcess = {
    unlearnedCards: Card[];
};

const StartLearningProcess = (cardSet: CardSet): LearningProcess => {
    return {
        unlearnedCards: [...cardSet.cards]
    };
};

const MarkCardAsLearned = (process: LearningProcess, cardId: string): LearningProcess => {
    const card = process.unlearnedCards.find(card => card.id === cardId);

    if (!card) 
    {
        return process;
    }

    const updatedUnlearnedCards = process.unlearnedCards.filter(card => card.id !== cardId);

    return {
        ...process,
        unlearnedCards: updatedUnlearnedCards 
    };
};

const MoveCardToBottom = (process: LearningProcess, cardId: string): LearningProcess => {
    const card = process.unlearnedCards.find(card => card.id === cardId);

    if (!card) 
    {
        return process;
    }

    const updatedUnlearnedCards = process.unlearnedCards.filter(card => card.id !== cardId);

    return {
        ...process,
        unlearnedCards: [...updatedUnlearnedCards, card] 
    };
};

export {
 StartLearningProcess,
 MarkCardAsLearned, 
 MoveCardToBottom
}