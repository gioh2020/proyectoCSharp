export interface satelite {
    sateliteId: number,
    sateliteName: string,
    coordinateX: number,
    coordinateY: number,

}

export interface MessageEncrypt {
    sateliteIdRef: number; 
    distance: number; 
    messageEncrypted: string; 
}

export interface sendData {
    satelites: satelite[] 
    messages: MessageEncrypt[];
}