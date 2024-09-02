import { useState } from "react";
import { CardSet } from "./types/CardSet";
import { AllCards } from "./components/AllCards";
import { ManageCardSets } from "./components/ManageCardSets";
import { useStore } from "./hooks/useStore";
import { LearningProcess } from "./components/LearningProcessView";

const App = () => {
  const { currentSet, actions } = useStore();
  const [mode, setMode] = useState<"start" | "manage" | "learn" | "allCards">("start");

  const handleStartLearning = () => {
    setMode("manage");
  };

  const handleSelectSet = (set: CardSet) => {
    actions.setCurrentSet(set);
    setMode("learn");
  };

  const handleBackToSets = () => {
    setMode("manage");
    actions.setCurrentSet(null);
  };

  const handleViewAllCards = () => {
    setMode("allCards");
  };

  const handleBackToLearning = () => {
    setMode("learn");
  };

  switch (mode) {
    case "start":
      return (
        <div>
          <h1>Welcome to the Card Learning App</h1>
          <button onClick={handleStartLearning}>Start Learning</button>
        </div>
      );

    case "manage":
      return <ManageCardSets handleSelectSet={handleSelectSet} />;

    case "learn":
      if (currentSet) {
        return (
          <LearningProcess
            currentSet={currentSet}
            handleBackToSets={handleBackToSets}
            handleViewAllCards={handleViewAllCards}
          />
        );
      }
      break;

    case "allCards":
      if (currentSet) {
        return <AllCards handleBackToLearning={handleBackToLearning} />;
      }
      break;

    default:
      return null;
  }
};

export default App;
