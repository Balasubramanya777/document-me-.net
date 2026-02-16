--
-- PostgreSQL database dump
--

\restrict ZOowMNvKZHGYtL327EtG8T3yianQOlpABGYYaIUd4Cqc28R02ifz75HpYegIXfJ

-- Dumped from database version 18.2
-- Dumped by pg_dump version 18.2

-- Started on 2026-02-16 21:59:55

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 222 (class 1259 OID 32980)
-- Name: document; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.document (
    document_id bigint NOT NULL,
    title character varying(50) NOT NULL,
    last_seen_at timestamp with time zone,
    created_by bigint,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    default_index integer
);


ALTER TABLE public.document OWNER TO postgres;

--
-- TOC entry 221 (class 1259 OID 32979)
-- Name: document_document_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.document ALTER COLUMN document_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.document_document_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    MAXVALUE 2147483647
    CACHE 1
);


--
-- TOC entry 223 (class 1259 OID 33036)
-- Name: document_snapshot; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.document_snapshot (
    document_id bigint NOT NULL,
    snapshot bytea,
    version bigint,
    updated_at timestamp with time zone
);


ALTER TABLE public.document_snapshot OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 33060)
-- Name: document_update; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.document_update (
    document_update_id bigint NOT NULL,
    document_id bigint NOT NULL,
    content bytea,
    created_by bigint,
    created_at timestamp with time zone
);


ALTER TABLE public.document_update OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 33059)
-- Name: document_update_document_update_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public.document_update ALTER COLUMN document_update_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.document_update_document_update_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 219 (class 1259 OID 32937)
-- Name: user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."user" (
    user_id bigint NOT NULL,
    user_name character varying(25) CONSTRAINT user_username_not_null NOT NULL,
    email character varying(50) NOT NULL,
    password text NOT NULL,
    is_active boolean,
    created_at timestamp with time zone,
    updated_at timestamp with time zone,
    first_name character varying(25) NOT NULL,
    last_name character varying(25) NOT NULL
);


ALTER TABLE public."user" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 32948)
-- Name: user_user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."user" ALTER COLUMN user_id ADD GENERATED ALWAYS AS IDENTITY (
    SEQUENCE NAME public.user_user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 4875 (class 2606 OID 33043)
-- Name: document_snapshot document_snapshot_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document_snapshot
    ADD CONSTRAINT document_snapshot_pkey PRIMARY KEY (document_id);


--
-- TOC entry 4877 (class 2606 OID 33068)
-- Name: document_update document_update_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document_update
    ADD CONSTRAINT document_update_pkey PRIMARY KEY (document_update_id);


--
-- TOC entry 4873 (class 2606 OID 32986)
-- Name: document documents_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document
    ADD CONSTRAINT documents_pkey PRIMARY KEY (document_id);


--
-- TOC entry 4871 (class 2606 OID 32947)
-- Name: user user_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT user_pkey PRIMARY KEY (user_id);


--
-- TOC entry 4878 (class 2606 OID 32989)
-- Name: document fk_document_created_by; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document
    ADD CONSTRAINT fk_document_created_by FOREIGN KEY (created_by) REFERENCES public."user"(user_id);


--
-- TOC entry 4880 (class 2606 OID 33069)
-- Name: document_update fk_document_created_by; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document_update
    ADD CONSTRAINT fk_document_created_by FOREIGN KEY (created_by) REFERENCES public."user"(user_id);


--
-- TOC entry 4879 (class 2606 OID 33044)
-- Name: document_snapshot fk_document_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document_snapshot
    ADD CONSTRAINT fk_document_id FOREIGN KEY (document_id) REFERENCES public.document(document_id);


--
-- TOC entry 4881 (class 2606 OID 33074)
-- Name: document_update fk_document_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.document_update
    ADD CONSTRAINT fk_document_id FOREIGN KEY (document_id) REFERENCES public.document(document_id);


-- Completed on 2026-02-16 21:59:55

--
-- PostgreSQL database dump complete
--

\unrestrict ZOowMNvKZHGYtL327EtG8T3yianQOlpABGYYaIUd4Cqc28R02ifz75HpYegIXfJ

