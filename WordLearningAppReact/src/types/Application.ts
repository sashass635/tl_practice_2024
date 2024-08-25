import { CardSet, CreateSet, DeleteSet } from "./CardSet";

export type Application = {
  cardsSet: CardSet[];
};

const AddNewSet = (app: Application, name: string): Application => {
  const newSet = CreateSet(name);
  return {
    ...app,
    cardsSet: [...app.cardsSet, newSet],
  };
};

const DeleteAppSet = (app: Application, id: string): Application => {
  return {
    ...app,
    cardsSet: DeleteSet(app.cardsSet, id),
  };
};

export { AddNewSet, DeleteAppSet };
