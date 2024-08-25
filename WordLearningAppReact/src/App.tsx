import { useState } from "react";
import { CardSet } from "./types/CardSet";
import { AllCards } from "./components/AllCards";
import { ManageCardSets } from "./components/ManageCardSets";
import { LearningProcess } from "./components/LearningProcessView";

const App = () => {
  const [application, setApplication] = useState({ cardsSet: [] as CardSet[] });
  const [currentSet, setCurrentSet] = useState<CardSet | null>(null);
  const [mode, setMode] = useState<"start" | "manage" | "learn" | "allCards">("start");

  const handleStartLearning = () => {
    setMode("manage");
  };

  const handleSelectSet = (set: CardSet) => {
    setCurrentSet(set);
    setMode("learn");
  };

  const handleBackToSets = () => {
    setMode("manage");
    setCurrentSet(null);
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
      return (
        <ManageCardSets application={application} setApplication={setApplication} handleSelectSet={handleSelectSet} />
      );

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
        return (
          <AllCards
            currentSet={currentSet}
            setCurrentSet={setCurrentSet}
            application={application}
            setApplication={setApplication}
            handleBackToLearning={handleBackToLearning}
          />
        );
      }
      break;

    default:
      return null;
  }
};

export default App;
