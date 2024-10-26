"use client";
import styles from "./page.module.scss";
import NavMenu from "@/components/navmenu/navmenu";
import Footer from "@/components/footer/footer";
import { AMBlueCard, AMLinkButton, AMMainCard, AMMainCleanCard, AMPrimaryRedButton, AMSecondaryButton, AMTextField } from "@/components/styledcomponents";
import { Typography, Card, IconButton, Dialog, DialogTitle, DialogContent } from "@mui/material";
import React, { useMemo } from "react";
import ReCAPTCHA from "react-google-recaptcha";
import { neueHaasUnicaProBold } from "./fonts";
import useIndex, { TIPOS_FORMULARIOS_LOGIN } from "./useindex";
import LoginForm from "@/smartcomponents/forms/loginform/loginform";
import RegistroFormDialog from "@/smartcomponents/dialogs/formdialogs/registroformdialog/registroformdialog";
import PasswordFormDialog from "@/smartcomponents/dialogs/formdialogs/passwordformdialog/passwordformdialog";
import ConfirmationFormDialog from "@/smartcomponents/dialogs/formdialogs/confirmationformdialog/confirmationformdialog";
import { SITE_KEY } from "./global";
import CloseIcon from "@mui/icons-material/Close";
import Loading from "./loading";
import SuccessDialog from "@/smartcomponents/successdialog/successdialog";
import ErrorDialog from "@/smartcomponents/errordialog/errordialog";
import WarningDialog from "@/smartcomponents/warningdialog/warningdialog";
import InfoDialog from "@/smartcomponents/infodialog/infodialog";
// Font files can be colocated inside of `pages`

export default function Home() {


  const {
  } = useIndex();



  return (
    <>
    

    </>
  );
}
