{{/* Set of required env-variables */}}
{{- define "secrets.env" -}}
- name: SOME_SECRET
  value: 'vault:weboffice/kv/data/{{ .Values.app.env | lower }}/Common/Mongo/Common#User'
{{- end -}}