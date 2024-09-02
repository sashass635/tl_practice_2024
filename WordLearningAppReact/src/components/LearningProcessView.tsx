import { useState } from "react";
import { CardSet } from "../types/CardSet";
import { useStore } from "../hooks/useStore";

type LearningProcessProps = {
  currentSet: CardSet;
  handleBackToSets: () => void;
  handleViewAllCards: () => void;
};

export const LearningProcess = ({ currentSet, handleBackToSets, handleViewAllCards }: LearningProcessProps) => {
  const { actions } = useStore();
  const [showTranslation, setShowTranslation] = useState(false);

  const handleToggleTranslation = () => {
    setShowTranslation(!showTranslation);
  };

  const handleNextCard = () => {
    setShowTranslation(false);
  };

  const handleMarkAsLearned = () => {
    actions.markCardAsLearned(currentSet.id);
    handleNextCard();
  };

  const handleMoveToBottom = () => {
    actions.moveCardToBottom(currentSet.id);
    handleNextCard();
  };


  return (
    <div>
      <h1>Learning Process</h1>
      {currentSet.cards.length > 0 ? (
        <div>
          <p>
            <strong>Word:</strong> {currentSet.cards[0].word}
          </p>
          {showTranslation && (
            <p>
              <strong>Translation:</strong> {currentSet.cards[0].translation}
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
