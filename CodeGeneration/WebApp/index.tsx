import React from "react";

export enum EnumIconColorVariant {
    green = "#A7E229",
    white = "#FFFFFF",
    blue = "#094FE0",
    pink = "#E4439F",
    yellow = "#FBB043",
    gray = "#9C9C9C",
}

/**
 * Значки для интерфейса
 * Добавлять новые элементы следует строго в конец списка
 */
export enum EnumIcon {
    arrow,
    menuHome,
    menuPlan,
    menuFacts,
    menuReport,
    menuReject,
    menuMasterData,
    menuAdminSettings,
    menuAbout,
    menuGantt,
    menuBar,
    statusDone,
    statusCross,
    statusInProcess,
    statusAttention,
    actionsMain,
    actionsLinks,
    actionsDelete,
    actionsPlus,
    actionsPlus2,
    actionsLook,
    actionsEdit,
    actionsMove,
    tableDrag,
    tableFilter,
    largeProduction,
    cip,
    dropdown,
    load,
    timeSeparator,
    excel,
    text,
    syncUser,
    history,
    allowMoveDown,
    menuRecipes,
    fileTextIcon,
    inputEnterButton,
    actionsDownload,
    cursorIcon,
    reloadIcon,
    largeDataMatrixIcon, //x
    largeAdmin,
    spiner,
    largeSpiner,
    largeAps,
    largeCh4,
    lagreGTS,
    menuInstrumentIcon,
    menuMeasurementIcon,
    menuMonitoringIcon,
    menuMapIcon,
    menuRemedyActionIcon,
    MoveUpIcon,
    MoveDownIcon,
    MoveLeftIcon,
    MoveRightIcon,
    user,
    time,
    calendar,
}

interface IProps extends IPropsSVGDefault {
    type: EnumIcon | undefined;
}

export type SvgImageType = {
    svg: string;
    tooltip?: string;
    color?: string;
};
