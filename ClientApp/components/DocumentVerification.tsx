//import * as React from "react";
//import * as DropZone from "react-dropzone";
//import { connect } from "react-redux";
//import { ActionCreator, bindActionCreators } from "redux";
//import * as _ from "lodash";
//import { RouteComponentProps } from 'react-router-dom';

//type WeatherForecastProps =
//    WeatherForecastsState.WeatherForecastsState        // ... state we've requested from the Redux store
//    & typeof WeatherForecastsState.actionCreators      // ... plus action creators we've requested
//    & RouteComponentProps<{ startDateIndex: string }>;

//class DocumentVerification extends React.Component<Props, {}> {
//    private handleDrop(files: any) {
        
//        const imgs = _.map(files, ({ name, path, size, type }: {name: any, path: any, size: any, type: any}) => {
//            return { name, path, size, type };
//        })

//        if (imgs.length) {
//            this.props.addImgs(imgs);
//        }

//    }

//    //private renderDropZoneInnerText({ isDragActive, isDragReject }) {
//    //    if (isDragActive) {
//    //        return <h4 className="drop-message">Omnomnom, let me have those videos!</h4>;
//    //    } else if (isDragReject) {
//    //        return <h4 className="drop-message">Uh oh, I don't know how to deal with that type of file!</h4>;
//    //    } else {
//    //        return <h4 className="drop-message">Drag and drop some files on me, or click to select.</h4>
//    //    }
//    //}

//    public render() {
//        return (
//            <div>
//                <h1>Hello Doc</h1>
//            </div>
//        );
//    }
//}

//export default connect((state) => ({}), dispatch => ({}))(DocumentVerification);