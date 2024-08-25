import { useState } from "react";
import { CardSet } from "../types/CardSet";
import { StartLearningProcess, MarkCardAsLearned, MoveCardToBottom } from "../types/LearningProcess";

type LearningProcessProps = {
  currentSet: CardSet;
  handleBackToSets: () => void;
  handleViewAllCards: () => void;
};

export const LearningProcess = ({ currentSet, handleBackToSets, handleViewAllCards }: LearningProcessProps) => {
  const [learningProcess, setLearningProcess] = useState(StartLearningProcess(currentSet));
  const [currentCardIndex, setCurrentCardIndex] = useState(0);
  const [showTranslation, setShowTranslation] = useState(false);

  const handleToggleTranslation = () => {
    setShowTranslation(!showTranslation);
  };

  const handleNextCard = () => {
    const randomIndex = Math.floor(Math.random() * learningProcess.unlearnedCards.length);
    setCurrentCardIndex(randomIndex);
    setShowTranslation(false);
  };

  const handleMarkAsLearned = () => {
    const updatedProcess = MarkCardAsLearned(learningProcess, learningProcess.unlearnedCards[currentCardIndex].id);
    setLearningProcess(updatedProcess);
    handleNextCard();
  };

  const handleMoveToBottom = () => {
    const updatedProcess = MoveCardToBottom(learningProcess, learningProcess.unlearnedCards[currentCardIndex].id);
    setLearningProcess(updatedProcess);
    handleNextCard();
  };

  return (
    <div>
      <h1>Learning Process</h1>
      {learningProcess.unlearnedCards.length > 0 ? (
        <div>
          <p>
            <strong>Word:</strong> {learningProcess.unlearnedCards[currentCardIndex].word}
          </p>
          {showTranslation && (
            <p>
              <strong>Translation:</strong> {learningProcess.unlearnedCards[currentCardIndex].translation}
            </p>
          )}
          <button onClick={handleToggleTranslation}>{showTranslation ? "Hide Translation" : "Show Translation"}</button>
          <button onClick={handleMarkAsLearned}>Mark as Learned</button>
          <button onClick={handleMoveToBottom}>Move to Bottom</button>
          <button onClick={handleViewAllCards}>View All Cards</button>
        </div>
      ) : (
        <div>
          <p>No cards to learn in this set.</p>
          <button onClick={handleViewAllCards}>Add New Card</button>
        </div>
      )}
      <footer>
        <button onClick={handleBackToSets}>Back to Sets</button>
      </footer>
    </div>
  );
};
