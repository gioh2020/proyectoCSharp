export interface satelite {
    sateliteId: number,
    sateliteName: string,
    coordenadax: number,
    coordenaday: number,

}

export interface MessageEncrypt {
    sateliteIdRef: number; 
    distance: number; 
    message: string; 
}

export interface sendData {
    satelites: satelite[] 
    messages: MessageEncrypt[];
}