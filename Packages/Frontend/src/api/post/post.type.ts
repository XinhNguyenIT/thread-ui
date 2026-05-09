import { PageNumberEnum } from "@/common/PageNumberEnum"

export type GetPageRequest = {
    page : number,
    pageSize: PageNumberEnum
}