import { HubConnection, HubConnectionBuilder, LogLevel } from "@microsoft/signalr";
import { ChatComment } from "../models/comment";
import { makeAutoObservable, runInAction } from "mobx";
import { store } from "./store";

export default class CommentStore {
    comments: ChatComment[] = []; //lista komentarzy
    hubConnection: HubConnection | null = null;

    constructor(){
        makeAutoObservable(this);
    }

    createHubConnection = (activityId: string) => {
        if(store.activityStore.selectedActivity){
            this.hubConnection = new HubConnectionBuilder()
                .withUrl('http://localhost:5000/chat?activityId='+activityId,{
                    accessTokenFactory: () => store.userStore.user?.token as string
                })
                .withAutomaticReconnect()
                .configureLogging(LogLevel.Information)
                .build();

                this.hubConnection.start().catch(error => console.log('Error estabilishing the connection: ', error));

                this.hubConnection.on("LoadComments", (comments:ChatComment[]) => {
                    runInAction(() => {
                        comments.forEach(comment => {
                            comment.createdAt = new Date(comment.createdAt + 'Z');
                        })
                        this.comments = comments
                    });
                })

                this.hubConnection.on('ReceiveComment', (comment:ChatComment) =>{
                    runInAction(() => {
                        comment.createdAt = new Date(comment.createdAt);
                        this.comments.unshift(comment)
                    });
                })
        }
    }

    stopHubConnection = () =>{
        this.hubConnection?.stop().catch(error => console.log("Error stopping connection: ", error));
    }

    clearComments = () =>{
        this.comments = [];
        this.stopHubConnection();
    }

    addComments = async (values: {body:string, activityId?:string}) =>{
        values.activityId = store.activityStore.selectedActivity?.id;
        try{
            await this.hubConnection?.invoke('SendComment', values);
            
        } catch(err){
            console.log(err)
        }
    }
    
}