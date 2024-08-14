import { Card } from "./Card";
import { CardSet} from "./CardSet"

export type LearningProcess = {
    learnedCards: Card[];
    unlearnedCards: Card[];
};

const StartLearningProcess = (cardSet: CardSet): LearningProcess => {
    return {
        learnedCards: [],
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
        learnedCards: [...process.learnedCards, card],
        unlearnedCards: updatedUnlearnedCards 
    };
};

const GetLearnedCards = (process: LearningProcess): Card[] => {
    return process.learnedCards;
}

export {
 StartLearningProcess,
 MarkCardAsLearned,
 GetLearnedCards
}