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

const MarkCardAsLearned = (process: LearningProcess, cardId: string, isCorrect: boolean): LearningProcess => {
    const card = process.unlearnedCards.find(card => card.id === cardId);
    
    if (!card) 
    {
        return process;
    }

    const updatedUnlearnedCards = process.unlearnedCards.filter(card => card.id !== cardId).concat(isCorrect ? [] : [card]);

    return {
        ...process,
        unlearnedCards: updatedUnlearnedCards 
    };
};

const GetUnlearnedCards = (process: LearningProcess): Card[] => {
    return process.unlearnedCards;
};

export {
 StartLearningProcess,
 MarkCardAsLearned,
 GetUnlearnedCards
}