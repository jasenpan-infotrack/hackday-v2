//import { fetch, addTask } from 'domain-task';
//import { Action, Reducer, ActionCreator } from 'redux';
//import { AppThunkAction } from './';

//// -----------------
//// STATE - This defines the type of data maintained in the Redux store.

//export interface Document {
//  isLoading: boolean;
//  documentType: number;
//  state: number;
//  name: string;
//  cardNumber: string;
//  address: string;
//  licenceNumber: string;
//  dateOfBirth: string;
//  expiryDate: string;
//}

//export interface UploadFile {
//    isUploading: boolean;
//    content: any;
//}


//// -----------------
//// ACTIONS - These are serializable (hence replayable) descriptions of state transitions.
//// They do not themselves have any side-effects; they just describe something that is going to happen.

//interface RequestWeatherForecastsAction {
//    type: 'REQUEST_UPLOAD_FILE';
//    payload: object
//}


//// ----------------
//// ACTION CREATORS - These are functions exposed to UI components that will trigger a state transition.
//// They don't directly mutate state, but they can have external side-effects (such as loading data).

//export const actionCreators = {
//    addImg: (img: any): AppThunkAction<any> => (dispatch, getState) => {
//        dispatch({ type: 'ADD_IMG_PENDING' });
//        fetch(`/api/Document/ValidPhoto`)
//            .then(response => response.json() as any)
//            .then(data => {
//                dispatch({ type: 'ADD_IMG_SUCCESS', payload: data });
//            });

//    }
//};

//// ----------------
//// REDUCER - For a given state and action, returns the new state. To support time travel, this must not mutate the old state.

//const unloadedState: WeatherForecastsState = { forecasts: [], isLoading: false };

//export const reducer: Reducer<WeatherForecastsState> = (state: WeatherForecastsState, incomingAction: Action) => {
//    const action = incomingAction as KnownAction;
//    switch (action.type) {
//        case 'REQUEST_WEATHER_FORECASTS':
//            return {
//                startDateIndex: action.startDateIndex,
//                forecasts: state.forecasts,
//                isLoading: true
//            };
//        case 'RECEIVE_WEATHER_FORECASTS':
//            // Only accept the incoming data if it matches the most recent request. This ensures we correctly
//            // handle out-of-order responses.
//            if (action.startDateIndex === state.startDateIndex) {
//                return {
//                    startDateIndex: action.startDateIndex,
//                    forecasts: action.forecasts,
//                    isLoading: false
//                };
//            }
//            break;
//        default:
//            // The following line guarantees that every action in the KnownAction union has been covered by a case above
//            const exhaustiveCheck: never = action;
//    }

//    return state || unloadedState;
//};

