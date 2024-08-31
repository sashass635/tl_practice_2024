import { CardSet, createSet, deleteSet } from "./CardSet"

export type Application ={
    cardsSet: CardSet[];
};

const addNewSet = (app: Application, name: string): Application => {
    const newSet = createSet(name);
    return {
        ...app,
        cardsSet: [...app.cardsSet, newSet]
    };
};

const deleteAppSet = (app: Application, id: string): Application => {
    return {
        ...app,
        cardsSet: deleteSet(app.cardsSet, id)
    };
}

export {
    addNewSet,
    deleteAppSet
}