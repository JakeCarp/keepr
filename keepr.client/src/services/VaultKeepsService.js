import { AppState } from "../AppState"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultKeepsService {
    async addToVault(keepId, vaultId) {
        const res = await api.post('api/vaultkeeps', { keepId: keepId, vaultId: vaultId })
        logger.log(res.data)
    }
    async removeFromVault(vaultKeepId) {
        const res = await api.delete('api/vaultkeeps/' + vaultKeepId)
        logger.log(res.data)
       AppState.keeps = AppState.keeps.filter(k => k.vaultKeepId != vaultKeepId)
    }
}
export const vaultKeepsService = new VaultKeepsService()