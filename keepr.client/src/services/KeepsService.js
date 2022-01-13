import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService {
    async getAllKeeps() {
       const res = await api.get('api/keeps')
        logger.log(res.data)
        AppState.keeps = res.data
    }
    async setActive(keep) {
        const res = await api.get('api/keeps/' + keep.id)
        logger.log(res.data)
        AppState.activeKeep = res.data
    }

    async getUserKeeps(id) {
        const res = await api.get('api/profiles/' + id + '/keeps')
        logger.log(res.data)
        AppState.keeps = res.data
    }
    async getMyKeeps() {
        const acctId = AppState.account.id
        const res = await api.get('api/profiles/' +acctId + '/keeps')
        logger.log(res.data)
        AppState.keeps = res.data
    }

    async createKeep(keepData) {
        const res = await api.post('api/keeps', keepData)
        logger.log(res.data)
        AppState.keeps.push(res.data)
    }
    
    async deleteKeep(keepId) {
        const res = await api.delete('api/keeps/' + keepId)
        logger.log(res.data)
        AppState.keeps = AppState.keeps.filter(k => k.id != keepId)
    }
}
export const keepsService = new KeepsService()